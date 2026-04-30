using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Scenes;
using System.Numerics;

var material = new MaterialBuilder("CubeMaterial")
    .WithDoubleSide(true)
    .WithMetallicRoughnessShader()
    .WithBaseColor(new Vector4(0.2f, 0.6f, 0.9f, 1.0f));

var mesh = new MeshBuilder<VertexPosition, VertexEmpty, VertexEmpty>("Cube");
var prim = mesh.UsePrimitive(material);

Vector3[] corners =
[
    new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f),
    new(0.5f,  0.5f, -0.5f), new(-0.5f,  0.5f, -0.5f),
    new(-0.5f, -0.5f,  0.5f), new(0.5f, -0.5f,  0.5f),
    new(0.5f,  0.5f,  0.5f), new(-0.5f,  0.5f,  0.5f)
];

int[][] faces =
[
    [0, 1, 2, 3], [4, 5, 6, 7], [0, 1, 5, 4],
    [2, 3, 7, 6], [0, 3, 7, 4], [1, 2, 6, 5]
];

foreach (var f in faces)
{
    var a = new VertexPosition(corners[f[0]]);
    var b = new VertexPosition(corners[f[1]]);
    var c = new VertexPosition(corners[f[2]]);
    var d = new VertexPosition(corners[f[3]]);
    prim.AddTriangle(a, b, c);
    prim.AddTriangle(a, c, d);
}

var sphereMaterial = new MaterialBuilder("SphereMaterial")
    .WithDoubleSide(true)
    .WithMetallicRoughnessShader()
    .WithBaseColor(new Vector4(0.9f, 0.3f, 0.3f, 1.0f));

var sphereMesh = new MeshBuilder<VertexPosition, VertexEmpty, VertexEmpty>("Sphere");
var spherePrim = sphereMesh.UsePrimitive(sphereMaterial);

int stacks = 16, slices = 24;
float radius = 0.5f;

for (int i = 0; i < stacks; i++)
{
    float phi1 = MathF.PI * i / stacks;
    float phi2 = MathF.PI * (i + 1) / stacks;

    for (int j = 0; j < slices; j++)
    {
        float theta1 = 2 * MathF.PI * j / slices;
        float theta2 = 2 * MathF.PI * (j + 1) / slices;

        var p1 = SphericalToCartesian(radius, phi1, theta1);
        var p2 = SphericalToCartesian(radius, phi1, theta2);
        var p3 = SphericalToCartesian(radius, phi2, theta2);
        var p4 = SphericalToCartesian(radius, phi2, theta1);

        spherePrim.AddTriangle(new VertexPosition(p1), new VertexPosition(p2), new VertexPosition(p3));
        spherePrim.AddTriangle(new VertexPosition(p1), new VertexPosition(p3), new VertexPosition(p4));
    }
}

var scene = new SceneBuilder();
scene.AddRigidMesh(mesh, Matrix4x4.CreateTranslation(-2, 0, 0));
scene.AddRigidMesh(sphereMesh, Matrix4x4.CreateTranslation(2, 0, 0));

var model = scene.ToGltf2();

model.SaveGLB("scene.glb");
Console.WriteLine("Wrote scene.glb ({0:N0} bytes) with cube + sphere", new FileInfo("scene.glb").Length);

scene = new SceneBuilder();
scene.AddRigidMesh(mesh, Matrix4x4.Identity);
var cubeModel = scene.ToGltf2();
cubeModel.SaveGLB("cube.glb");
Console.WriteLine("Wrote cube.glb ({0:N0} bytes)", new FileInfo("cube.glb").Length);

static Vector3 SphericalToCartesian(float r, float phi, float theta) =>
    new(r * MathF.Sin(phi) * MathF.Cos(theta),
        r * MathF.Cos(phi),
        r * MathF.Sin(phi) * MathF.Sin(theta));
