using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinBitTest.Controllers.Dtos
{
    public class ValueDtoConteverter : JsonConverter<ICollection<ClientValueDto>>
    {
        public override ICollection<ClientValueDto> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var collection = new List<ClientValueDto>();
            reader.Read();
            while(reader.Read())
            {
                var dto = new ClientValueDto();

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    reader.Read();
                    return collection;
                }

                var propertyName = reader.GetString();
                dto.Code = propertyName;

                // Get the value.
                reader.Read();
                var value = reader.GetString();
                dto.Value = value;
                collection.Add(dto);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, ICollection<ClientValueDto> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
