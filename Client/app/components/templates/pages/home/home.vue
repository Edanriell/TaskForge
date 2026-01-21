<script lang="ts" setup>
	import { StatisticCard } from "@molecules";
	import { ProjectCard } from "@organisms";

	import type { ProjectStatus } from "@organisms";

	const projects = [
		{
			id: 1,
			name: "Project Alpha",
			tasks: 8,
			completedTasks: 3,
			status: "Active" as ProjectStatus
		},
		{
			id: 2,
			name: "Project Beta",
			tasks: 12,
			completedTasks: 12,
			status: "Completed" as ProjectStatus
		},
		{
			id: 3,
			name: "Project Gamma",
			tasks: 5,
			completedTasks: 0,
			status: "Paused" as ProjectStatus
		}
	];

	const stats = [
		{ label: "Total Projects", value: 3, icon: "heroicons:folder", color: "text-primary" },
		{
			label: "Active Tasks",
			value: 25,
			icon: "heroicons:clipboard-document-list",
			color: "text-info"
		},
		{ label: "Completed", value: 15, icon: "heroicons:check-circle", color: "text-success" },
		{ label: "Overdue", value: 2, icon: "heroicons:exclamation-circle", color: "text-error" }
	];

	const handleEdit = (id: number) => {
		console.log("Edit project:", id);
		// TODO: Open edit modal or navigate to edit page
	};

	const handleDuplicate = (id: number) => {
		console.log("Duplicate project:", id);
		// TODO: Duplicate project logic
	};

	const handleDelete = (id: number) => {
		console.log("Delete project:", id);
		// TODO: Show confirmation modal and delete
	};
</script>

<template>
	<section class="p-6 lg:p-8">
		<div class="mb-8">
			<h1 class="text-3xl font-bold">Welcome back! 👋</h1>
			<p class="text-base-content/60 mt-1">Here's what's happening with your projects.</p>
		</div>
		<div class="mb-10 grid grid-cols-2 gap-4 lg:grid-cols-4">
			<StatisticCard
				v-for="stat in stats"
				:key="stat.label"
				:label="stat.label"
				:value="stat.value"
				:icon="stat.icon"
				:color="stat.color"
				class="card bg-base-100 border border-base-300 shadow-sm"
			/>
		</div>
		<div class="mb-6 flex items-center justify-between">
			<div>
				<h2 class="text-xl font-bold">Your Projects</h2>
				<p class="text-base-content/60 text-sm">{{ projects.length }} projects total</p>
			</div>
			<button class="btn btn-primary btn-sm gap-2">
				<Icon name="heroicons:plus" class="h-4 w-4" />
				New Project
			</button>
		</div>
		<div class="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
			<ProjectCard
				v-for="project in projects"
				:key="project.id"
				:project="project"
				@edit="handleEdit"
				@duplicate="handleDuplicate"
				@delete="handleDelete"
			/>
		</div>
	</section>
</template>
