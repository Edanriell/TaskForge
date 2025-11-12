"use client"

<script lang="ts" setup>
	const projects = [
		{
			id: 1,
			name: "Project One",
			tasks: ["Task One", "Task Two"]
		},
		{
			id: 2,
			name: "Project Two",
			tasks: ["Task One", "Task Two", "Task Three", "Task Four"]
		}
	];
</script>

<template>
	<aside class="drawer lg:drawer-open">
		<input id="app-drawer" class="drawer-toggle" type="checkbox" >

		<!-- Page Content -->
		<div class="drawer-content flex flex-col items-center justify-center">
			<label class="btn btn-primary drawer-button lg:hidden mt-4" for="app-drawer">
				<Icon name="material-symbols:menu" class="w-12 h-12" />
				Menu
			</label>
		</div>

		<!-- Sidebar -->
		<div class="drawer-side">
			<label class="drawer-overlay" for="app-drawer" />

			<div
				class="min-h-full w-80 bg-base-200 p-5 flex flex-col justify-between border-r border-base-300"
			>
				<!-- Top Section -->
				<div>
					<h2 class="text-2xl font-extrabold mb-8 tracking-tight px-2">TaskForge</h2>

					<ul class="menu">
						<li class="mb-2">
							<NuxtLink
								class="font-semibold text-base hover:bg-base-300 rounded-lg px-3 py-2"
								to="/"
							>
								Overview
							</NuxtLink>
						</li>

						<li class="menu-title mt-6 text-gray-500 uppercase text-xs tracking-wider">
							Projects
						</li>

						<!-- Project Accordion -->
						<li
							v-for="project in projects"
							:key="project.id"
							class="mt-3 border-l-2 border-transparent hover:border-primary transition-all"
						>
							<details class="group" open>
								<summary
									class="font-semibold text-base cursor-pointer hover:bg-base-300 rounded-l-none! rounded-r-md! px-3 py-2 flex items-center justify-between"
								>
									<span>{{ project.name }}</span>
								</summary>

								<ul
									class="relative mt-2 pl-5 space-y-1 before:content-[''] before:absolute before:left-2 before:top-2 before:h-[90%] before:w-[2px] before:bg-base-300 hover:before:bg-[#4129d3] before:opacity-[1] transition-all duration-200"
								>
									<li
										v-for="(task, index) in project.tasks"
										:key="index"
										class="text-sm"
									>
										<a class="block px-2 py-1 rounded-md hover:bg-base-300">
											{{ task }}
										</a>
									</li>

									<!-- New Task Button -->
									<li class="mt-3">
										<button
											class="btn btn-xs btn-outline btn-primary w-full"
											onclick="createTaskModal.showModal()"
										>
											+ New Task
										</button>
									</li>
								</ul>
							</details>
						</li>
					</ul>
				</div>

				<!-- Bottom Section -->
				<div class="mt-8 border-t border-base-300 pt-4">
					<button
						class="btn btn-sm btn-neutral w-full"
						onclick="createProjectModal.showModal()"
					>
						+ New Project
					</button>
				</div>
			</div>
		</div>

		<!-- Create Task Modal -->
		<dialog id="createTaskModal" class="modal">
			<div class="modal-box">
				<form method="dialog">
					<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
						✕
					</button>
				</form>
				<h3 class="text-lg font-bold mb-4">Create Task</h3>
				<input
					class="input input-bordered w-full mb-3"
					placeholder="Task name"
					type="text"
				>
				<button class="btn btn-primary w-full">Save Task</button>
			</div>
		</dialog>

		<!-- Create Project Modal -->
		<dialog id="createProjectModal" class="modal">
			<div class="modal-box">
				<form method="dialog">
					<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
						✕
					</button>
				</form>
				<h3 class="text-lg font-bold mb-4">Create Project</h3>
				<input
					class="input input-bordered w-full mb-3"
					placeholder="Project name"
					type="text"
				>
				<button class="btn btn-primary w-full">Save Project</button>
			</div>
		</dialog>
	</aside>
</template>
