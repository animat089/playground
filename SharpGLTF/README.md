# SharpGLTF: 3D Models in Code

Generates glTF binary files (.glb) from C# using SharpGLTF.Toolkit. Builds a cube and a UV sphere, positions them in a scene, and writes the output to disk.

## Prerequisites

- .NET 8 SDK

No Docker, no external tools.

## Run

```bash
cd AnimatLabs.SharpGLTF
dotnet run
```

Expected output:

```text
Wrote scene.glb (X bytes) with cube + sphere
Wrote cube.glb (X bytes)
```

Open `cube.glb` or `scene.glb` in Windows 3D Viewer, or drag into https://gltf-viewer.donmccurdy.com/.

## What It Uses

- [SharpGLTF.Toolkit](https://www.nuget.org/packages/SharpGLTF.Toolkit) (MIT)

## Project Structure

```
AnimatLabs.SharpGLTF/
  Program.cs          # cube + sphere generation, scene assembly, .glb export
  AnimatLabs.SharpGLTF.csproj
```
