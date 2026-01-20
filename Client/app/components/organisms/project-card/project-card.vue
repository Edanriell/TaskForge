<script lang="ts" setup>
	import { AppBadge, AppProgressBar } from "@atoms";

	interface Project {
		id: number;
		name: string;
		tasks: number;
		completedTasks: number;
		status: "Active" | "Paused" | "Completed";
	}

	interface Props {
		project: Project;
	}

	const props = defineProps<Props>();

	const emit = defineEmits<{
		edit: [id: number];
		duplicate: [id: number];
		delete: [id: number];
	}>();

	const statusConfig = {
		Active: { variant: "success" as const, icon: "heroicons:play-circle" },
		Paused: { variant: "warning" as const, icon: "heroicons:pause-circle" },
		Completed: { variant: "neutral" as const, icon: "heroicons:check-circle" }
	};

	const progressPercentage = computed(() => {
		if (props.project.tasks === 0) return 0;
		return Math.round((props.project.completedTasks / props.project.tasks) * 100);
	});

	const currentStatus = computed(() => statusConfig[props.project.status]);
</script>

<template>
	<div
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
				<AppBadge :variant="currentStatus.variant" :icon="currentStatus.icon">
					{{ project.status }}
				</AppBadge>
			</div>

			<!-- Progress -->
			<div class="mb-4">
				<div class="mb-1 flex justify-between text-xs">
					<span class="text-base-content/60">Progress</span>
					<span class="font-medium">
						{{ project.completedTasks }}/{{ project.tasks }} tasks
					</span>
				</div>
				<AppProgressBar
					:value="progressPercentage"
					:variant="project.status === 'Completed' ? 'success' : 'primary'"
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
							<a class="gap-2" @click="emit('edit', project.id)">
								<Icon name="heroicons:pencil" class="h-4 w-4" /> Edit
							</a>
						</li>
						<li>
							<a class="gap-2" @click="emit('duplicate', project.id)">
								<Icon name="heroicons:document-duplicate" class="h-4 w-4" />
								Duplicate
							</a>
						</li>
						<li>
							<a class="gap-2 text-error" @click="emit('delete', project.id)">
								<Icon name="heroicons:trash" class="h-4 w-4" /> Delete
							</a>
						</li>
					</ul>
				</div>
			</div>
		</div>
	</div>
</template>
