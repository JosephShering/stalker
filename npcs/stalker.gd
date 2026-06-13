extends NPC

var energy := 20
var has_heal_item := true

func idle(delta, blackboard) -> int:
	return HTNAction.SUCCESS
	
func walk_to_point(delta, blackboard) -> int:
	return HTNAction.SUCCESS
	
func target_closest_open_campfire(delta, blackboard) -> int:
	return HTNAction.SUCCESS
