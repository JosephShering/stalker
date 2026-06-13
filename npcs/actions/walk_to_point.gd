class_name WalkToTarget
extends HTNAction

func _enter(data: Blackboard) -> void:
	print("Walking to point")
	
func _update(data: Blackboard, delta: float) -> int:
	return HTNAction.SUCCESS
	
func _exit(data: Blackboard) -> void:
	pass
