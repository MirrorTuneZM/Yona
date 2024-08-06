﻿using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace Yona.Core.Common.Serializers;

public class YamlFileSerializer : IFileSerializer
{
    public static readonly YamlFileSerializer Instance = new();

    private static readonly ISerializer Serializer = new SerializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .DisableAliases()
        .Build();

    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .IgnoreUnmatchedProperties()
        .Build();

    public T DeserializeFile<T>(string filePath) => Deserializer.Deserialize<T>(File.ReadAllText(filePath));

    public void SerializeFile<T>(string filePath, T obj) => File.WriteAllText(filePath, Serializer.Serialize(obj));
}
