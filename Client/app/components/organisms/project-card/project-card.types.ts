export type ProjectStatus = "Active" | "Paused" | "Completed";

export interface Project {
	id: number;
	name: string;
	tasks: number;
	completedTasks: number;
	status: ProjectStatus;
}
