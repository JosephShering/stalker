class_name SitAtCampfire
extends HTNAction

var campfire_seat: CampfireSeat

func _enter(_data: Blackboard) -> void:
	print("Sitting at Campfire")
	
func _update(_data: Blackboard, _delta: float) -> int:
	print("Updating Sit at Campfire")
	return HTNAction.SUCCESS

func _exit(data: Blackboard) -> void:
	var seat_path: String = data.get("target_seat")
	if seat_path == null: return

	var seat: CampfireSeat = get_tree().root.get_node(seat_path)
	seat.unclaim()
