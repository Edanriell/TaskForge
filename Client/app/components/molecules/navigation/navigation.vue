<template>
	<div class="mt-14">
		<!-- Navigation -->
		<ul class="menu">
			<li class="mb-2">
				<!-- Link -->
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

			<!-- Accordion -->
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
						class="relative mt-2 pl-5 space-y-1 before:content-[''] before:absolute before:left-2 before:top-2 before:h-[90%] before:w-[1.5px] before:bg-base-300 hover:before:bg-[#4129d3] before:opacity-[1] transition-all duration-200"
					>
						<li
							v-for="(task, index) in project.tasks"
							:key="index"
							class="text-sm"
						>
							<a class="block px-4 py-1 rounded-md hover:bg-base-300">
								{{ task }}
							</a>
						</li>

						<!-- Button -->
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
</template>

<script lang="ts" setup>

</script>
