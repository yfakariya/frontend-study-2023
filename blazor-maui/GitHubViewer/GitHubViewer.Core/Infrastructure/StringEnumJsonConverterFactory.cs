// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;
using Octokit;

namespace GitHubViewer.Infrastructure
{
	internal sealed class StringEnumJsonConverterFactory : JsonConverterFactory
	{
		public static StringEnumJsonConverterFactory Instance { get; } = new StringEnumJsonConverterFactory();

		private StringEnumJsonConverterFactory() { }

		public override bool CanConvert(Type typeToConvert)
			=> typeToConvert.IsGenericType
			&& typeToConvert.GetGenericTypeDefinition() == typeof(StringEnum<>)
			&& typeToConvert.GetGenericArguments()[0].IsValueType;

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
			=> (JsonConverter)typeof(StringEnumJsonConver<>)
				.MakeGenericType(typeToConvert.GetGenericArguments()[0])
				.GetField("Instance")!
				.GetValue(null)!;

		private sealed class StringEnumJsonConver<T> : JsonConverter<StringEnum<T>>
			where T : struct
		{
			public static readonly StringEnumJsonConver<T> Instance = new StringEnumJsonConver<T>();

			private StringEnumJsonConver() { }

			public override StringEnum<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (reader.TokenType != JsonTokenType.String && reader.TokenType != JsonTokenType.Null)
				{
					throw new JsonException("Token must be String.");
				}

				var str = reader.GetString();
				if (str == null)
				{
					return default;
				}

				return new StringEnum<T>(str);
			}

			public override void Write(Utf8JsonWriter writer, StringEnum<T> value, JsonSerializerOptions options)
			{
				if (String.IsNullOrEmpty(value.StringValue))
				{
					writer.WriteNullValue();
				}
				else
				{
					writer.WriteStringValue(value.StringValue);
				}
			}
		}
	}
}
