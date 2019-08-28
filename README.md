# gltf.tooling

Global tooling for getting detailed (like vertice position, normals and attributes) information about glTF files

## Installation

- Install from NuGet

https://www.nuget.org/packages/gltf.tooling/

```
$ dotnet tool install -g gltf.tooling
```

or update:

```
$ dotnet tool update -g gltf.tooling

```

## Running

1] Command Info gltf_file 

Example:

```
$ gltf info test.glb

File: test.glb
Generator: SharpGLTF 1.0.0
Version: 2.0
Primitive type: TRIANGLES
Vertex accessors: POSITION,NORMAL,_CUSTOM_1,COLOR_0,TEXCOORD_0
Vertex accessor: POSITION
4373.1924, 5522.678, -359.8238
4370.978, 5522.723, -359.89185
4364.6157, 5511.5107, -359.08923
Vertex accessor: NORMAL
0.57735026, 0.57735026, 0.57735026
0.57735026, 0.57735026, 0.57735026
0.57735026, 0.57735026, 0.57735026
Vertex accessor: _CUSTOM_1
100
101
102
Vertex accessor: COLOR_0
1,1, 1, 1
1,1, 1, 1
1,1, 1, 1
Vertex accessor: TEXCOORD_0
texcoord_0
0, 0
0, 0
0, 0
```
