import os
import bpy
import sys
import pathlib
from glob import glob

sys.path.append(os.path.dirname(__file__))

import blender_argparse

arg_parser = blender_argparse.ArgumentParserForBlender()
arg_parser.add_argument("-s", "--source")
arg_parser.add_argument("-d", "--destination")

args = arg_parser.parse_args()
if not args.source or not args.destination:
    bpy.ops.wm.quit_blender()

source = pathlib.Path(args.source)
if not os.path.exists(args.destination):
    os.makedirs(args.destination)

for path in source.rglob("*.fbx"):
    bpy.ops.wm.read_homefile(use_empty=True)

    bpy.ops.import_scene.fbx(filepath=str(path.resolve()), use_image_search=True)
    bpy.ops.object.transform_apply()

    try:
        bpy.ops.file.find_missing_files(directory=str(source.resolve()))
    except Exception as e:
        print(e)

    bpy.ops.object.select_by_type(type="MESH")
    for obj in bpy.context.selected_objects:
        obj.name = obj.name + "-col"

    bpy.ops.wm.save_as_mainfile(filepath=os.path.join(args.destination,  f"{path.stem}.blend"))

bpy.ops.wm.quit_blender()
