<script lang="ts" setup>
	import { Logotype, Modal, Button, Input } from "@atoms";
	import { Navigation } from "@molecules";

	// Event comes from DaisyUi
	const handleOpenCreateProjectModal = () => {
		(document.getElementById("createProjectModal") as HTMLDialogElement)?.showModal();
	};
</script>

<template>
	<aside class="drawer lg:drawer-open">
		<input id="app-drawer" class="drawer-toggle" type="checkbox" />
		<div class="drawer-content flex flex-col">
			<div class="lg:hidden sticky top-0 z-40 w-full">
				<div class="flex h-16 items-center px-4">
					<label class="btn btn-ghost btn-square" for="app-drawer" aria-label="Open menu">
						<Icon name="mdi:menu" class="h-6 w-6" />
					</label>
					<Logotype classes="flex pointer-events-none select-none" />
				</div>
			</div>
		</div>
		<div class="drawer-side">
			<label class="drawer-overlay" for="app-drawer" />
			<div
				class="min-h-full w-80 bg-base-200 p-5 flex flex-col justify-between border-r border-base-300"
			>
				<Logotype classes="hidden absolute pointer-events-none select-none lg:flex" />
				<div class="mt-14">
					<Navigation />
				</div>
				<div class="mt-8 border-t border-base-300 pt-4">
					<Button classes="w-full" color="black" @click="handleOpenCreateProjectModal">
						+ New Project
					</Button>
				</div>
			</div>
		</div>
		<Modal id="createTaskModal" title="Create Task">
			<Input placeholder="Task name" />
			<Button color="blue" classes="btn-sm w-full">Save Task</Button>
		</Modal>
		<Modal id="createProjectModal" title="Create Project">
			<Input placeholder="Project name" />
			<Button color="blue" classes="w-full">Save Project</Button>
		</Modal>
	</aside>
</template>
