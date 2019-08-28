using SharpGLTF.Schema2;
using System;
using System.Reflection;

namespace gltf.tooling
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

                Console.WriteLine($"gltf v{versionString}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                Console.WriteLine("  gltf info <glb-file>");
                return;
            }

            if (args[0] == "info")
            {
                Info(args[1]);
            }
        }

        private static void Info(string file)
        {
            Console.WriteLine("File: " + file);
            var model = ModelRoot.Load(file);
            Console.WriteLine("Generator: " + model.Asset.Generator);
            Console.WriteLine("Version: " + model.Asset.Version);

            foreach(var mesh in model.LogicalMeshes)
            {
                foreach(var primitive in mesh.Primitives)
                {
                    Console.WriteLine("Primitive type: " + primitive.DrawPrimitiveType);

                    Console.WriteLine("Vertex accessors: " + String.Join(',', primitive.VertexAccessors.Keys));

                    foreach(var vertexAccessor in primitive.VertexAccessors)
                    {
                        var va = primitive.GetVertexAccessor(vertexAccessor.Key);
                        Console.WriteLine("Vertex accessor: " + vertexAccessor.Key);

                        if (vertexAccessor.Key == "POSITION" || vertexAccessor.Key == "NORMAL")
                        {
                            var vectors = va.AsVector3Array();
                            foreach(var v in vectors)
                            {
                                Console.WriteLine(v.X + ", "+ v.Y + ", "+ v.Z);
                            }
                        }
                        else if (vertexAccessor.Key == "COLOR_0")
                        {
                            var colors = va.AsColorArray();
                            foreach(var c in colors)
                            {
                                Console.WriteLine($"{c.X},{c.Y}, {c.Z}, {c.W}");
                            }
                        }
                        else if (vertexAccessor.Key == "TEXCOORD_0")
                        {
                            Console.WriteLine("texcoord_0");
                            var texcoords = va.AsVector2Array();
                            foreach(var texcoord in texcoords)
                            {
                                Console.WriteLine(texcoord.X + ", " + texcoord.Y);
                            }
                        }

                        else if (vertexAccessor.Key.StartsWith("_")){
                            // custom attribute
                            var scalars = va.AsScalarArray();
                            foreach (var s in scalars)
                            {
                                Console.WriteLine(s);
                            }

                        }
                    }
                }
            }
        }
    }
}
