<script lang="ts" setup>
	import { Badge, ProgressBar } from "@atoms";

	export interface Project {
		id: number;
		name: string;
		tasks: number;
		completedTasks: number;
		status: "Active" | "Paused" | "Completed";
	}

	interface Props {
		project: Project;
		showActions?: boolean;
		linkPrefix?: string;
	}

	const props = withDefaults(defineProps<Props>(), {
		showActions: true,
		linkPrefix: "/projects"
	});

	const emit = defineEmits<{
		edit: [id: number];
		duplicate: [id: number];
		delete: [id: number];
	}>();

	const statusConfig: Record<
		Project["status"],
		{ variant: "success" | "warning" | "neutral"; icon: string }
	> = {
		Active: { variant: "success", icon: "heroicons:play-circle" },
		Paused: { variant: "warning", icon: "heroicons:pause-circle" },
		Completed: { variant: "neutral", icon: "heroicons:check-circle" }
	};

	const progressPercentage = computed(() => {
		if (props.project.tasks === 0) return 0;
		return Math.round((props.project.completedTasks / props.project.tasks) * 100);
	});

	const currentStatus = computed(() => statusConfig[props.project.status]);

	const taskLabel = computed(() => (props.project.tasks === 1 ? "task" : "tasks"));

	const progressVariant = computed(() =>
		props.project.status === "Completed" ? "success" : "primary"
	);

	const projectLink = computed(() => `${props.linkPrefix}/${props.project.id}`);
</script>

<template>
	<article
		class="card bg-base-100 border border-base-300 shadow-sm transition-all duration-200 hover:shadow-md hover:border-primary/30"
	>
		<div class="card-body p-5">
			<header class="mb-3 flex items-start justify-between">
				<div class="flex items-center gap-3">
					<div class="rounded-lg bg-primary/10 p-2" aria-hidden="true">
						<Icon name="heroicons:folder" class="h-5 w-5 text-primary" />
					</div>
					<div>
						<h3 class="font-semibold">{{ project.name }}</h3>
						<p class="text-base-content/60 text-xs">
							{{ project.tasks }} {{ taskLabel }}
						</p>
					</div>
				</div>
				<Badge :variant="currentStatus.variant" :icon="currentStatus.icon" size="sm">
					{{ project.status }}
				</Badge>
			</header>
			<div class="mb-4">
				<div class="mb-1 flex justify-between text-xs">
					<span class="text-base-content/60">Progress</span>
					<span class="font-medium">
						{{ project.completedTasks }}/{{ project.tasks }} {{ taskLabel }}
					</span>
				</div>
				<ProgressBar :value="progressPercentage" :variant="progressVariant" />
			</div>
			<footer v-if="showActions" class="card-actions justify-end">
				<NuxtLink :to="projectLink" class="btn btn-primary btn-sm"> Open Project </NuxtLink>
				<div class="dropdown dropdown-end">
					<button
						type="button"
						class="btn btn-ghost btn-sm btn-square"
						aria-label="Project options"
						aria-haspopup="true"
					>
						<Icon name="heroicons:ellipsis-vertical" class="h-4 w-4" />
					</button>
					<ul
						class="menu dropdown-content z-10 w-40 rounded-box bg-base-100 p-2 shadow-lg border border-base-300"
						role="menu"
					>
						<li role="menuitem">
							<button type="button" class="gap-2" @click="emit('edit', project.id)">
								<Icon name="heroicons:pencil" class="h-4 w-4" /> Edit
							</button>
						</li>
						<li role="menuitem">
							<button
								type="button"
								class="gap-2"
								@click="emit('duplicate', project.id)"
							>
								<Icon name="heroicons:document-duplicate" class="h-4 w-4" />
								Duplicate
							</button>
						</li>
						<li role="menuitem">
							<button
								type="button"
								class="gap-2 text-error"
								@click="emit('delete', project.id)"
							>
								<Icon name="heroicons:trash" class="h-4 w-4" /> Delete
							</button>
						</li>
					</ul>
				</div>
			</footer>
		</div>
	</article>
</template>
