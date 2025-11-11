<script lang="ts" setup>
	const projects = [
		{ id: 1, name: "Project Alpha", tasks: 8, status: "Active" },
		{ id: 2, name: "Project Beta", tasks: 12, status: "Paused" },
		{ id: 3, name: "Project Gamma", tasks: 5, status: "Completed" }
	];
</script>

<template>
	<section class="px-6 py-12">
		<div class="flex items-center justify-between mb-8">
			<h2 class="text-3xl font-bold">Projects <strong>(3)</strong></h2>
		</div>

		<div class="space-y-4">
			<div
				v-for="project in projects"
				:key="project.id"
				class="card bg-base-100 border border-base-300 rounded-xl shadow-sm p-2 space-y-5 hover:shadow-md transition-all duration-200"
			>
				<div
					class="card-body flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4"
				>
					<!-- Project Info -->
					<div>
						<h2 class="card-title text-lg font-semibold">{{ project.name }}</h2>
						<p class="text-sm text-gray-600">
							{{ project.tasks }} {{ project.tasks === 1 ? "task" : "tasks" }}
						</p>
					</div>

					<!-- Status + Actions -->
					<div class="flex flex-wrap items-center gap-3">
						<span
							:class="{
								'badge badge-success': project.status === 'Active',
								'badge badge-warning': project.status === 'Paused',
								'badge badge-neutral': project.status === 'Completed'
							}"
						>
							{{ project.status }}
						</span>

						<NuxtLink
							:to="`/projects/${project.id}`"
							class="btn btn-sm btn-outline btn-primary"
						>
							View
						</NuxtLink>
						<button class="btn btn-sm btn-outline btn-accent">Edit</button>
						<button class="btn btn-sm btn-outline btn-error">Delete</button>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>
