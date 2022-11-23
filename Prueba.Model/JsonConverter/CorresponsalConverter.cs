using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Prueba.Model.JsonConverter
{
    public class CorresponsalConverter : JsonConverter<Corresponsal>
    {
        public override Corresponsal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonObject? jsonNode = (JsonObject?)JsonNode.Parse(ref reader);

            var Corresponsal = new Corresponsal()
            {
                CorCorresponsalId = (long)jsonNode["CorCorresponsalId"],
                CorNombre = (string)jsonNode["CorNombre"]
            };

            var hashSetTemp = new HashSet<Oficina>();
            foreach (var oficinaNode in jsonNode["Oficinas"].AsArray())
            {
                hashSetTemp.Add(new Oficina()
                {
                    OfiId = (long)oficinaNode["OfiId"],
                    OfiCorresponsalId = (long)oficinaNode["OfiCorresponsalId"],
                    OfiNombre = (string)oficinaNode["OfiNombre"]

                });
            }

            return Corresponsal;
        }

        public override void Write(Utf8JsonWriter writer, Corresponsal value, JsonSerializerOptions options)
        {
            var corresponsal = value;

            writer.WriteStartObject();

            writer.WriteNumber("CorCorresponsalId", corresponsal.CorCorresponsalId);
            writer.WriteString("CorNombre", corresponsal.CorNombre);

            writer.WritePropertyName("Oficinas");
            writer.WriteStartArray();
            foreach (var oficina in corresponsal.Oficinas)
            {
                writer.WriteStartObject();
                writer.WriteNumber("OfiId", oficina.OfiId);
                writer.WriteNumber("OfiCorresponsalId", oficina.OfiCorresponsalId);
                writer.WriteString("OfiNombre", oficina.OfiNombre);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
