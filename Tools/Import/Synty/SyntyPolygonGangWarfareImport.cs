using Godot;

// This sample changes all node names.
// Called right after the scene is imported and gets the root node.
[Tool]
public partial class SyntyPolygonGangWarfareImport : EditorScenePostImport
{
    bool _isVehicle = false;

    public override GodotObject _PostImport(Node scene)
    {
        _isVehicle = scene.Name.ToString().ToLower().Contains("_veh");

        Iterate(scene);
        return scene;
    }

    public void Iterate(Node node)
    {
        if (node == null)
        {
            return;
        }

        if (node is MeshInstance3D)
        {
            Mesh mesh = (node as MeshInstance3D).Mesh;
            if (node.Name.ToString().ToLower().Contains("_glass"))
            {
                mesh.SurfaceSetMaterial(
                    0,
                    ResourceLoader.Load<StandardMaterial3D>("res://assets/Materials/Glass.tres")
                );
            }
            else if (_isVehicle)
            {
                mesh.SurfaceSetMaterial(
                    0,
                    ResourceLoader.Load<StandardMaterial3D>(
                        "res://assets/PolygonGangWarfare/Materials/PolygonGangWarfareVehicle01A.tres"
                    )
                );
            }
            else
            {
                mesh.SurfaceSetMaterial(
                    0,
                    ResourceLoader.Load<StandardMaterial3D>(
                        "res://assets/PolygonGangWarfare/Materials/PolygonGangWarfare01A.tres"
                    )
                );
            }
        }

        foreach (Node child in node.GetChildren())
        {
            Iterate(child);
        }
    }
}
