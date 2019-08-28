using SharpGLTF.Schema2;
using SharpGLTF.Validation;
using System;
using System.Linq;
using System.Reflection;

namespace gltf.tooling
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showPositions = false;
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
                if(args.Length == 3 && args[2] == "showPositions")
                {
                    showPositions = true;
                }
                Info(args[1], showPositions);
            }
        }

        private static void Info(string file, bool showPositions)
        {
            Console.WriteLine("File: " + file);

            try
            {
                var model = ModelRoot.Load(file);
                Console.WriteLine("Generator: " + model.Asset.Generator);
                Console.WriteLine("Version: " + model.Asset.Version);

                HandleModel(model, showPositions);
            }
            catch(LinkException e)
            {
                Console.WriteLine("Error loading model: " + e.Message);
            }

        }

        private static void HandleModel(ModelRoot model, bool showPositions)
        {
            Console.WriteLine("Number of meshes: " + model.LogicalMeshes.Count);
            foreach (var mesh in model.LogicalMeshes)
            {
                Console.WriteLine("Number of primitives: " + mesh.Primitives.Count);
                foreach (var primitive in mesh.Primitives)
                {
                    Console.WriteLine("Primitive type: " + primitive.DrawPrimitiveType);

                    var indexaccessor = primitive.IndexAccessor;
                    Console.WriteLine("Vertex accessors: " + String.Join(',', primitive.VertexAccessors.Keys));

                    if (primitive.VertexAccessors.Count > 0)
                    {
                        Console.WriteLine("Number of triangles: " + primitive.GetVertexAccessor("POSITION").AsVector3Array().Count / 3);
                    }

                    foreach (var vertexAccessor in primitive.VertexAccessors)
                    {
                        var va = primitive.GetVertexAccessor(vertexAccessor.Key);
                        Console.WriteLine("Vertex accessor: " + vertexAccessor.Key);

                        if (vertexAccessor.Key == "POSITION" || vertexAccessor.Key == "NORMAL")
                        {
                            if (showPositions)
                            {
                                var vectors = va.AsVector3Array();
                                foreach (var v in vectors)
                                {
                                    Console.WriteLine(v.X + ", " + v.Y + ", " + v.Z);
                                }
                            }
                        }
                        else if (vertexAccessor.Key == "COLOR_0")
                        {
                            var colors = va.AsColorArray();
                            foreach (var c in colors)
                            {
                                Console.WriteLine($"{c.X},{c.Y}, {c.Z}, {c.W}");
                            }
                        }
                        else if (vertexAccessor.Key == "TEXCOORD_0")
                        {
                            Console.WriteLine("texcoord_0");
                            var texcoords = va.AsVector2Array();
                            foreach (var texcoord in texcoords)
                            {
                                Console.WriteLine(texcoord.X + ", " + texcoord.Y);
                            }
                        }

                        else if (vertexAccessor.Key.StartsWith("_"))
                        {
                            Console.WriteLine("Custom vertex attribute: " + vertexAccessor.Key);

                            var scalars = va.AsScalarArray();
                            Console.WriteLine($"Number of scalars of {vertexAccessor.Key}: " + scalars.Count);
                            var result = scalars.Distinct();
                            Console.WriteLine($"Distinct scalar values of {vertexAccessor.Key}: {String.Join(',', result)}");
                        }
                    }
                }
            }
        }
    }
}
