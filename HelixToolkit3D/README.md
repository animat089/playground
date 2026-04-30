# 3D Shapes with WPF and Helix Toolkit

Renders five 3D primitives (cube, sphere, cone, cylinder, torus) in a WPF window using Helix Toolkit. Orbit camera, lighting, and coordinate axes included out of the box.

## Prerequisites

- Windows
- .NET 8 SDK
- No Docker, no database, no external services

## Run

```bash
cd AnimatLabs.HelixToolkit3D
dotnet run
```

A window opens with five colored shapes. Click and drag to orbit. Scroll to zoom.

## What It Uses

- `HelixToolkit.Wpf` 3.1.2 (MIT license)
- WPF `HelixViewport3D` control with `DefaultLights`
- Shape primitives: `BoxVisual3D`, `SphereVisual3D`, `TruncatedConeVisual3D`, `TorusVisual3D`

## Project Structure

```
AnimatLabs.HelixToolkit3D/
  MainWindow.xaml        -- viewport and layout
  MainWindow.xaml.cs     -- shape creation
  App.xaml               -- WPF app entry
```

Total code: ~70 lines of C# and ~12 lines of XAML.
