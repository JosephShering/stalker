class_name WalkToTarget
extends HTNAction

@export var agent: NPC
@export var nav: NavigationAgent3D


func _update(data: Blackboard, _delta: float) -> int:
	var target_position: Vector3 = data.get("target_position")
	if target_position == null:
		print("Target position is null")
		return HTNAction.FAILED

	nav.target_position = target_position

	if not nav.is_target_reachable():
		print("Target not reachable")
		return HTNAction.FAILED

	if nav.is_navigation_finished():
		print("Navigation finished")
		return HTNAction.SUCCESS

	var next_path_position := nav.get_next_path_position()
	var direction := agent.global_position.direction_to(next_path_position)

	agent.velocity = Vector3(direction.x * agent.speed, agent.velocity.y, direction.z * agent.speed)
	agent.move_and_slide()

	return HTNAction.ONGOING


func _exit(_data: Blackboard) -> void:
	print("Exiting WalkToTarget action")