<script lang="ts" setup>
	import { useRoute } from "vue-router";

	const route = useRoute();
	const projectId = route.params.projectId;

	const project = ref({
		id: projectId,
		name: "Project Alpha",
		description: "A task management app for teams, built with Nuxt and Tailwind.",
		status: "Active",
		createdAt: "Jan 5, 2026",
		dueDate: "Feb 15, 2026",
		tasks: [
			{
				id: 1,
				title: "Setup project repo",
				completed: true,
				priority: "High",
				dueDate: "Jan 8"
			},
			{
				id: 2,
				title: "Implement UI layout",
				completed: false,
				priority: "High",
				dueDate: "Jan 15"
			},
			{
				id: 3,
				title: "Integrate API",
				completed: false,
				priority: "Medium",
				dueDate: "Jan 20"
			},
			{
				id: 4,
				title: "Write unit tests",
				completed: false,
				priority: "Low",
				dueDate: "Jan 25"
			}
		]
	});

	const completedTasks = computed(() => project.value.tasks.filter((t) => t.completed).length);
	const totalTasks = computed(() => project.value.tasks.length);
	const progressPercentage = computed(() => {
		if (totalTasks.value === 0) return 0;
		return Math.round((completedTasks.value / totalTasks.value) * 100);
	});

	const getStatusConfig = (status: string) => {
		const configs: Record<string, { class: string; icon: string }> = {
			Active: { class: "badge-success", icon: "heroicons:play-circle" },
			Paused: { class: "badge-warning", icon: "heroicons:pause-circle" },
			Completed: { class: "badge-neutral", icon: "heroicons:check-circle" }
		};
		return configs[status] || configs.Active;
	};

	const getPriorityConfig = (priority: string) => {
		const configs: Record<string, { class: string; icon: string }> = {
			High: { class: "text-error", icon: "heroicons:chevron-double-up" },
			Medium: { class: "text-warning", icon: "heroicons:minus" },
			Low: { class: "text-info", icon: "heroicons:chevron-double-down" }
		};
		return configs[priority] || configs.Medium;
	};

	const toggleTask = (taskId: number) => {
		const task = project.value.tasks.find((t) => t.id === taskId);
		if (task) task.completed = !task.completed;
	};
</script>

<template>
	<section class="p-6 lg:p-8">
		<!-- Breadcrumb -->
		<div class="breadcrumbs mb-6 text-sm">
			<ul>
				<li>
					<NuxtLink to="/" class="gap-1 text-base-content/60 hover:text-primary">
						<Icon name="heroicons:home" class="h-4 w-4" />
						Home
					</NuxtLink>
				</li>
				<li>
					<span class="font-medium">{{ project.name }}</span>
				</li>
			</ul>
		</div>

		<!-- Header Card -->
		<div class="card mb-8 border border-base-300 bg-base-100 shadow-sm">
			<div class="card-body p-6">
				<div class="flex flex-col gap-6 lg:flex-row lg:items-start lg:justify-between">
					<!-- Project Info -->
					<div class="flex items-start gap-4">
						<div class="rounded-xl bg-primary/10 p-4">
							<Icon name="heroicons:folder" class="h-8 w-8 text-primary" />
						</div>
						<div>
							<div class="mb-2 flex flex-wrap items-center gap-3">
								<h1 class="text-2xl font-bold">{{ project.name }}</h1>
								<span
									:class="['badge gap-1', getStatusConfig(project.status).class]"
								>
									<Icon
										:name="getStatusConfig(project.status).icon"
										class="h-3 w-3"
									/>
									{{ project.status }}
								</span>
							</div>
							<p class="text-base-content/60 mb-4 max-w-xl">
								{{ project.description }}
							</p>
							<div class="flex flex-wrap gap-4 text-sm">
								<div class="flex items-center gap-1 text-base-content/60">
									<Icon name="heroicons:calendar" class="h-4 w-4" />
									Created: {{ project.createdAt }}
								</div>
								<div class="flex items-center gap-1 text-base-content/60">
									<Icon name="heroicons:clock" class="h-4 w-4" />
									Due: {{ project.dueDate }}
								</div>
							</div>
						</div>
					</div>

					<!-- Actions -->
					<div class="flex gap-2">
						<button class="btn btn-outline btn-sm gap-2">
							<Icon name="heroicons:pencil" class="h-4 w-4" />
							Edit
						</button>
						<div class="dropdown dropdown-end">
							<div tabindex="0" role="button" class="btn btn-ghost btn-sm btn-square">
								<Icon name="heroicons:ellipsis-vertical" class="h-5 w-5" />
							</div>
							<ul
								tabindex="0"
								class="menu dropdown-content z-10 w-44 rounded-box border border-base-300 bg-base-100 p-2 shadow-lg"
							>
								<li>
									<a class="gap-2">
										<Icon name="heroicons:archive-box" class="h-4 w-4" />
										Archive
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

				<!-- Progress Bar -->
				<div class="mt-6 border-t border-base-300 pt-6">
					<div class="mb-2 flex items-center justify-between">
						<span class="text-sm font-medium">Overall Progress</span>
						<span class="text-sm font-bold">{{ progressPercentage }}%</span>
					</div>
					<progress
						:value="progressPercentage"
						:class="[
							'progress h-3 w-full',
							progressPercentage === 100 ? 'progress-success' : 'progress-primary'
						]"
						max="100"
					/>
					<p class="text-base-content/60 mt-2 text-xs">
						{{ completedTasks }} of {{ totalTasks }} tasks completed
					</p>
				</div>
			</div>
		</div>

		<!-- Tasks Section -->
		<div class="mb-5 flex items-center justify-between">
			<div>
				<h2 class="text-xl font-bold">Tasks</h2>
				<p class="text-base-content/60 text-sm">
					{{ totalTasks }} tasks · {{ completedTasks }} completed
				</p>
			</div>
			<button class="btn btn-primary btn-sm gap-2">
				<Icon name="heroicons:plus" class="h-4 w-4" />
				Add Task
			</button>
		</div>

		<!-- Task List -->
		<div class="space-y-3">
			<div
				v-for="task in project.tasks"
				:key="task.id"
				:class="[
					'card border bg-base-100 shadow-sm transition-all duration-200 hover:shadow-md',
					task.completed
						? 'border-base-300 opacity-70'
						: 'border-base-300 hover:border-primary/30'
				]"
			>
				<div class="card-body flex-row items-center justify-between gap-4 p-4">
					<!-- Left: Checkbox + Title -->
					<div class="flex min-w-0 flex-1 items-center gap-4">
						<input
							:checked="task.completed"
							class="checkbox checkbox-primary"
							type="checkbox"
							@change="toggleTask(task.id)"
						/>
						<div class="min-w-0 flex-1">
							<h3
								:class="[
									'font-medium truncate',
									task.completed ? 'line-through text-base-content/50' : ''
								]"
							>
								{{ task.title }}
							</h3>
							<div class="mt-1 flex items-center gap-3 text-xs">
								<span
									:class="[
										'flex items-center gap-1',
										getPriorityConfig(task.priority).class
									]"
								>
									<Icon
										:name="getPriorityConfig(task.priority).icon"
										class="h-3 w-3"
									/>
									{{ task.priority }}
								</span>
								<span class="text-base-content/50 flex items-center gap-1">
									<Icon name="heroicons:calendar" class="h-3 w-3" />
									{{ task.dueDate }}
								</span>
							</div>
						</div>
					</div>

					<!-- Right: Status + Actions -->
					<div class="flex items-center gap-3">
						<span
							:class="[
								'badge badge-sm',
								task.completed ? 'badge-success' : 'badge-ghost'
							]"
						>
							{{ task.completed ? "Done" : "Pending" }}
						</span>
						<NuxtLink
							:to="`/projects/${projectId}/tasks/${task.id}`"
							class="btn btn-ghost btn-sm btn-square"
						>
							<Icon name="heroicons:arrow-right" class="h-4 w-4" />
						</NuxtLink>
					</div>
				</div>
			</div>
		</div>

		<!-- Empty State (when no tasks) -->
		<div v-if="project.tasks.length === 0" class="mt-8 text-center">
			<div class="mx-auto mb-4 w-fit rounded-full bg-base-200 p-6">
				<Icon
					name="heroicons:clipboard-document-list"
					class="h-12 w-12 text-base-content/40"
				/>
			</div>
			<h3 class="mb-2 font-semibold">No tasks yet</h3>
			<p class="text-base-content/60 mb-4 text-sm">Get started by creating your first task</p>
			<button class="btn btn-primary btn-sm gap-2">
				<Icon name="heroicons:plus" class="h-4 w-4" />
				Add Task
			</button>
		</div>
	</section>
</template>
