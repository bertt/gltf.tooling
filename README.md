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
Generator:
Version: 2.0
Number of meshes: 1
Number of primitives: 1
Primitive type: TRIANGLES
Vertex accessors: NORMAL,POSITION,_BATCHID
Number of triangles: 2631
Vertex accessor: NORMAL
Vertex accessor: POSITION
Vertex accessor: _BATCHID
Custom vertex attribute: _BATCHID
Number of scalars of _BATCHID: 7893
Distinct scalar values of _BATCHID: 0,1
```

To print the positions and normals add 'showPositions' to the command:

```
$ gltf info test.glb showPositions
```
