<script lang="ts" setup>
	import { useRoute } from "vue-router";

	const route = useRoute();
	const projectId = route.params.projectId;
	const taskId = route.params.taskId;

	const task = ref({
		id: taskId,
		title: "Design homepage UI",
		description:
			"Create responsive homepage layout for TaskForge using Tailwind and DaisyUI. The design should be clean, modern, and follow our brand guidelines. Include mobile-first approach with proper breakpoints.",
		project: { id: projectId, name: "Project Beta" },
		completed: false,
		priority: "High",
		dueDate: "2026-02-01",
		createdAt: "2026-01-10"
	});

	const toggleCompletion = () => {
		task.value.completed = !task.value.completed;
	};

	const getPriorityConfig = (priority: string) => {
		const configs: Record<string, { class: string; bgClass: string; icon: string }> = {
			High: {
				class: "text-error",
				bgClass: "bg-error/10",
				icon: "heroicons:chevron-double-up"
			},
			Medium: { class: "text-warning", bgClass: "bg-warning/10", icon: "heroicons:minus" },
			Low: {
				class: "text-info",
				bgClass: "bg-info/10",
				icon: "heroicons:chevron-double-down"
			}
		};
		return configs[priority] ?? configs.Medium;
	};

	const formatDate = (dateString: string) => {
		return new Date(dateString).toLocaleDateString("en-US", {
			month: "short",
			day: "numeric",
			year: "numeric"
		});
	};

	const getDaysRemaining = (dueDate: string) => {
		const today = new Date();
		const due = new Date(dueDate);
		const diffTime = due.getTime() - today.getTime();
		return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
	};

	const daysRemaining = computed(() => getDaysRemaining(task.value.dueDate));
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
					<NuxtLink
						:to="`/projects/${task.project.id}`"
						class="text-base-content/60 hover:text-primary"
					>
						{{ task.project.name }}
					</NuxtLink>
				</li>
				<li>
					<span class="font-medium">{{ task.title }}</span>
				</li>
			</ul>
		</div>

		<!-- Layout: Card + Actions -->
		<div class="flex flex-col items-start gap-6 lg:flex-row">
			<!-- Task Card -->
			<div class="card w-full flex-1 border border-base-300 bg-base-100 shadow-sm">
				<div class="card-body p-6">
					<!-- Header -->
					<div class="flex items-start gap-4">
						<button
							:class="[
								'mt-1 flex h-10 w-10 shrink-0 items-center justify-center rounded-full border-2 transition-all',
								task.completed
									? 'border-success bg-success text-success-content'
									: 'border-base-300 hover:border-primary'
							]"
							@click="toggleCompletion"
						>
							<Icon v-if="task.completed" name="heroicons:check" class="h-6 w-6" />
						</button>
						<div>
							<h1
								:class="[
									'text-2xl font-bold',
									task.completed ? 'line-through text-base-content/50' : ''
								]"
							>
								{{ task.title }}
							</h1>
							<div class="mt-2 flex flex-wrap items-center gap-2">
								<span
									:class="[
										'badge',
										task.completed ? 'badge-success' : 'badge-ghost'
									]"
								>
									{{ task.completed ? "Completed" : "Pending" }}
								</span>
								<span
									:class="[
										'badge gap-1',
										getPriorityConfig(task.priority).bgClass,
										getPriorityConfig(task.priority).class
									]"
								>
									<Icon
										:name="getPriorityConfig(task.priority).icon"
										class="h-3 w-3"
									/>
									{{ task.priority }}
								</span>
							</div>
						</div>
					</div>

					<!-- Description -->
					<div class="mt-8">
						<h3 class="mb-2 text-sm font-medium text-base-content/60">Description</h3>
						<p class="text-base-content/80 leading-relaxed">
							{{ task.description }}
						</p>
					</div>

					<!-- Divider -->
					<div class="divider" />

					<!-- Meta Info -->
					<div class="grid gap-4 sm:grid-cols-3">
						<!-- Project -->
						<div>
							<p class="mb-1 text-xs font-medium text-base-content/60">Project</p>
							<NuxtLink
								:to="`/projects/${task.project.id}`"
								class="flex items-center gap-2 font-medium hover:text-primary"
							>
								<Icon name="heroicons:folder" class="h-4 w-4 text-primary" />
								{{ task.project.name }}
							</NuxtLink>
						</div>

						<!-- Due Date -->
						<div>
							<p class="mb-1 text-xs font-medium text-base-content/60">Due Date</p>
							<div class="flex items-center gap-2">
								<Icon
									name="heroicons:calendar"
									:class="[
										'h-4 w-4',
										daysRemaining < 0
											? 'text-error'
											: daysRemaining <= 3
												? 'text-warning'
												: 'text-base-content/60'
									]"
								/>
								<span class="font-medium">{{ formatDate(task.dueDate) }}</span>
							</div>
							<p
								:class="[
									'mt-1 text-xs',
									daysRemaining < 0
										? 'text-error'
										: daysRemaining <= 3
											? 'text-warning'
											: 'text-base-content/50'
								]"
							>
								<template v-if="daysRemaining < 0">
									{{ Math.abs(daysRemaining) }} days overdue
								</template>
								<template v-else-if="daysRemaining === 0"> Due today </template>
								<template v-else> {{ daysRemaining }} days remaining </template>
							</p>
						</div>

						<!-- Created -->
						<div>
							<p class="mb-1 text-xs font-medium text-base-content/60">Created</p>
							<div class="flex items-center gap-2">
								<Icon name="heroicons:clock" class="h-4 w-4 text-base-content/60" />
								<span class="font-medium">{{ formatDate(task.createdAt) }}</span>
							</div>
						</div>
					</div>
				</div>
			</div>

			<!-- Actions (outside card) -->
			<div class="flex w-full shrink-0 flex-row gap-2 lg:w-3xs lg:flex-col">
				<button class="btn btn-outline btn-sm flex-1 gap-2 lg:flex-none">
					<Icon name="heroicons:pencil" class="h-4 w-4" />
					Edit
				</button>
				<button class="btn btn-outline btn-error btn-sm flex-1 gap-2 lg:flex-none">
					<Icon name="heroicons:trash" class="h-4 w-4" />
					Delete
				</button>
			</div>
		</div>
	</section>
</template>
