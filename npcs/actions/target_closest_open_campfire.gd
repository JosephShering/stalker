class_name TargetClosestOpenCampfire
extends HTNAction

func _enter(data: Blackboard) -> void:
	Campfires.closest()
	print("Targetting closest open campfire")
