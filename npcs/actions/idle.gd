class_name Idle
extends HTNAction

func _enter(_data: Blackboard) -> void:
	print("Enter Idle Action")
	
func _update(_data: Blackboard, _delta: float) -> int:
	return HTNAction.SUCCESS
	
func _exit(_data: Blackboard) -> void:
	pass
