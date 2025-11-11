<script lang="ts" setup>
	import { useRoute } from "vue-router";

	const route = useRoute();
	const projectId = route.params.id;

	// Example project (replace later with API call)
	const project = ref({
		id: projectId,
		name: "Project Alpha",
		description: "A task management app for teams, built with Nuxt and Tailwind.",
		status: "Active",
		tasks: [
			{ id: 1, title: "Setup project repo", completed: true },
			{ id: 2, title: "Implement UI layout", completed: false },
			{ id: 3, title: "Integrate API", completed: false }
		]
	});
</script>

<template>
	<section class="px-6 py-12">
		<!-- Header -->
		<div class="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8 gap-4">
			<div>
				<h1 class="text-4xl font-extrabold mb-2 tracking-tight">
					{{ project.name }}
				</h1>
				<p class="text-gray-600 text-base leading-relaxed max-w-2xl">
					{{ project.description }}
				</p>
			</div>

			<div class="flex gap-2">
				<button class="btn btn-accent btn-sm sm:btn-md">Edit</button>
				<button class="btn btn-error btn-sm sm:btn-md">Delete</button>
			</div>
		</div>

		<!-- Status -->
		<div class="mb-10">
			<span
				:class="{
					'badge badge-success badge-lg': project.status === 'Active',
					'badge badge-warning badge-lg': project.status === 'Paused',
					'badge badge-neutral badge-lg': project.status === 'Completed'
				}"
			>
				{{ project.status }}
			</span>
		</div>

		<!-- Tasks -->
		<div class="flex items-center justify-between mb-5">
			<h2 class="text-2xl font-semibold">Tasks</h2>
		</div>

		<div class="space-y-4">
			<div
				v-for="task in project.tasks"
				:key="task.id"
				class="card bg-base-100 border border-base-300 rounded-xl shadow-sm p-2 space-y-5 hover:shadow-md transition-all duration-200"
			>
				<div
					class="card-body flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 py-4"
				>
					<div class="flex items-center gap-3">
						<input
							:checked="task.completed"
							class="checkbox checkbox-sm"
							disabled
							type="checkbox"
						>
						<h3
							:class="[
								'font-medium text-lg',
								task.completed ? 'line-through text-gray-500' : ''
							]"
						>
							{{ task.title }}
						</h3>
					</div>

					<div class="flex items-center gap-3">
						<span
							:class="{
								'badge badge-success': task.completed,
								'badge badge-error': !task.completed
							}"
						>
							{{ task.completed ? "Done" : "Pending" }}
						</span>
						<NuxtLink
							:to="`/tasks/${task.id}`"
							class="btn btn-sm btn-outline btn-primary"
						>
							View
						</NuxtLink>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>
