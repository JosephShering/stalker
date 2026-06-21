class_name TargetClosestOpenCampfire
extends HTNAction

@export var agent: NPC

func _enter(_data: Blackboard) -> void:
	print("Targetting Closest Open Campfire")

func _update(data: Blackboard, _delta: float) -> int:
	print("Running update")
	var campfire := Campfires.closest_one(agent.global_position)
	if campfire == null: return HTNAction.FAILED

	var open_seat := campfire.get_open_seat()
	if open_seat == null: return HTNAction.FAILED

	open_seat.claim()

	data.set("target_seat", str(open_seat.get_path()))
	data.set("target_position", campfire.global_position)

	return HTNAction.SUCCESS
