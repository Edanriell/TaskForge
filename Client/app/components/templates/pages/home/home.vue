<script lang="ts" setup>
	const projects = [
		{ id: 1, name: "Project Alpha", tasks: 8, completedTasks: 3, status: "Active" },
		{ id: 2, name: "Project Beta", tasks: 12, completedTasks: 12, status: "Completed" },
		{ id: 3, name: "Project Gamma", tasks: 5, completedTasks: 0, status: "Paused" }
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

	const getProgressPercentage = (completed: number, total: number) => {
		if (total === 0) return 0;
		return Math.round((completed / total) * 100);
	};

	const getStatusConfig = (status: string) => {
		const configs: Record<string, { class: string; icon: string }> = {
			Active: { class: "badge-success", icon: "heroicons:play-circle" },
			Paused: { class: "badge-warning", icon: "heroicons:pause-circle" },
			Completed: { class: "badge-neutral", icon: "heroicons:check-circle" }
		};
		return configs[status] || configs.Active;
	};
</script>

<template>
	<section class="p-6 lg:p-8">
		<!-- Header -->
		<div class="mb-8">
			<h1 class="text-3xl font-bold">Welcome back! 👋</h1>
			<p class="text-base-content/60 mt-1">Here's what's happening with your projects.</p>
		</div>

		<!-- Stats Grid -->
		<div class="mb-10 grid grid-cols-2 gap-4 lg:grid-cols-4">
			<div
				v-for="stat in stats"
				:key="stat.label"
				class="card bg-base-100 border border-base-300 shadow-sm"
			>
				<div class="card-body p-4">
					<div class="flex items-center justify-between">
						<div>
							<p class="text-base-content/60 text-sm">{{ stat.label }}</p>
							<p class="text-2xl font-bold">{{ stat.value }}</p>
						</div>
						<div class="rounded-xl bg-base-200 p-3">
							<Icon :name="stat.icon" :class="['h-6 w-6', stat.color]" />
						</div>
					</div>
				</div>
			</div>
		</div>

		<!-- Projects Section -->
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

		<!-- Project Cards -->
		<div class="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
			<div
				v-for="project in projects"
				:key="project.id"
				class="card bg-base-100 border border-base-300 shadow-sm transition-all duration-200 hover:shadow-md hover:border-primary/30"
			>
				<div class="card-body p-5">
					<!-- Header -->
					<div class="mb-3 flex items-start justify-between">
						<div class="flex items-center gap-3">
							<div class="rounded-lg bg-primary/10 p-2">
								<Icon name="heroicons:folder" class="h-5 w-5 text-primary" />
							</div>
							<div>
								<h3 class="font-semibold">{{ project.name }}</h3>
								<p class="text-base-content/60 text-xs">
									{{ project.tasks }} {{ project.tasks === 1 ? "task" : "tasks" }}
								</p>
							</div>
						</div>
						<span
							:class="['badge badge-sm gap-1', getStatusConfig(project.status).class]"
						>
							<Icon :name="getStatusConfig(project.status).icon" class="h-3 w-3" />
							{{ project.status }}
						</span>
					</div>

					<!-- Progress -->
					<div class="mb-4">
						<div class="mb-1 flex justify-between text-xs">
							<span class="text-base-content/60">Progress</span>
							<span class="font-medium">
								{{ project.completedTasks }}/{{ project.tasks }} tasks
							</span>
						</div>
						<progress
							:value="getProgressPercentage(project.completedTasks, project.tasks)"
							:class="[
								'progress h-2 w-full',
								project.status === 'Completed'
									? 'progress-success'
									: 'progress-primary'
							]"
							max="100"
						/>
					</div>

					<!-- Actions -->
					<div class="card-actions justify-end">
						<NuxtLink :to="`/projects/${project.id}`" class="btn btn-primary btn-sm">
							Open Project
						</NuxtLink>
						<div class="dropdown dropdown-end">
							<div tabindex="0" role="button" class="btn btn-ghost btn-sm btn-square">
								<Icon name="heroicons:ellipsis-vertical" class="h-4 w-4" />
							</div>
							<ul
								tabindex="0"
								class="menu dropdown-content z-10 w-40 rounded-box bg-base-100 p-2 shadow-lg border border-base-300"
							>
								<li>
									<a class="gap-2">
										<Icon name="heroicons:pencil" class="h-4 w-4" />
										Edit
									</a>
								</li>
								<li>
									<a class="gap-2">
										<Icon name="heroicons:document-duplicate" class="h-4 w-4" />
										Duplicate
									</a>
								</li>
								<li>
									<a class="gap-2 text-error">
										<Icon name="heroicons:trash" class="h-4 w-4" />
										Delete
									</a>
								</li>
							</ul>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>
