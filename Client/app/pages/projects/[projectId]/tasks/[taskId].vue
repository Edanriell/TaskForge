<script lang="ts" setup>
	import { useRoute } from "vue-router";

	const route = useRoute();
	const taskId = route.params.id;

	// Later you'll fetch this from API or store
	const task = ref({
		id: taskId,
		title: "Design homepage UI",
		description: "Create responsive homepage layout for TaskForge using Tailwind and DaisyUI.",
		project: { id: 2, name: "Project Beta" },
		completed: false,
		dueDate: "2025-12-01"
	});

	// Handle toggling (placeholder for now)
	const toggleCompletion = () => {
		task.value.completed = !task.value.completed;
	};
</script>

<template>
	<section class="px-6 py-12">
		<!-- Header -->
		<div class="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8 gap-4">
			<div>
				<h1 class="text-4xl font-extrabold mb-2 tracking-tight">
					{{ task.title }}
				</h1>
				<p class="text-gray-600 text-base leading-relaxed max-w-2xl">
					{{ task.description }}
				</p>
			</div>

			<div class="flex gap-2">
				<button class="btn btn-accent btn-sm sm:btn-md">Edit</button>
				<button class="btn btn-error btn-sm sm:btn-md">Delete</button>
			</div>
			<div>SadTask</div>
		</div>

		<!-- Task Info Card -->
		<div
			class="bg-base-100 border border-base-300 rounded-xl shadow-sm p-6 space-y-5 hover:shadow-md transition-all duration-200"
		>
			<div class="flex items-center justify-between flex-wrap gap-4">
				<div class="flex items-center gap-3">
					<input
						:checked="task.completed"
						class="checkbox checkbox-lg"
						type="checkbox"
						@change="toggleCompletion"
					>
					<span
						:class="[
							'font-semibold text-lg',
							task.completed ? 'line-through text-gray-500' : 'text-base-content'
						]"
					>
						{{ task.completed ? "Task Completed" : "Task In Progress" }}
					</span>
				</div>

				<span
					:class="{
						'badge badge-success badge-lg': task.completed,
						'badge badge-error badge-lg': !task.completed
					}"
				>
					{{ task.completed ? "Done" : "Pending" }}
				</span>
			</div>

			<div class="divider my-4" />

			<div class="grid sm:grid-cols-2 gap-4">
				<div>
					<p class="text-sm font-medium text-gray-500 mb-1">Project</p>
					<NuxtLink
						:to="`/projects/${task.project.id}`"
						class="link link-primary font-semibold"
					>
						{{ task.project.name }}
					</NuxtLink>
				</div>

				<div>
					<p class="text-sm font-medium text-gray-500 mb-1">Due Date</p>
					<p class="font-semibold text-base">
						{{ new Date(task.dueDate).toLocaleDateString() }}
					</p>
				</div>
			</div>
		</div>
	</section>
</template>
