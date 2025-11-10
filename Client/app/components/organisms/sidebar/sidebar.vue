"use client"

<script lang="ts" setup></script>

<template>
	<aside class="drawer lg:drawer-open">
		<input id="my-drawer-3" class="drawer-toggle" type="checkbox" />
		<div class="drawer-content flex flex-col items-center justify-center">
			<!-- Page content here -->
			<label class="btn drawer-button lg:hidden" for="my-drawer-3"> Open drawer </label>
		</div>
		<div class="drawer-side">
			<label aria-label="close sidebar" class="drawer-overlay" for="my-drawer-3"></label>
			<ul class="menu bg-base-200 min-h-full w-80 p-4">
				<li class="mb-4"><a>Overview</a></li>
				<li>
					<details open>
						<summary>Project One</summary>
						<ul>
							<li><a>Task One</a></li>
							<li><a>Task Two</a></li>
							<li>
								<button
									class="btn btn-sm btn-dash mt-2"
									onclick="create_task_modal.showModal()"
								>
									New Task
								</button>
							</li>
						</ul>
					</details>
					<details class="mt-2" open>
						<summary>Project Two</summary>
						<ul>
							<li><a>Task One</a></li>
							<li><a>Task Two</a></li>
							<li><a>Task Three</a></li>
							<li><a>Task Four</a></li>
							<li>
								<button
									class="btn btn-sm btn-dash mt-2"
									onclick="create_task_modal.showModal()"
								>
									New Task
								</button>
							</li>
						</ul>
					</details>
				</li>
				<li>
					<button
						class="btn btn-sm btn-neutral mt-6"
						onclick="create_project_modal.showModal()"
					>
						New Project
					</button>
				</li>
			</ul>
		</div>
		<dialog id="create_task_modal" class="modal">
			<div class="modal-box">
				<form method="dialog">
					<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
						✕
					</button>
				</form>
				<h3 class="text-lg font-bold">Create Task Modal</h3>
				<p class="py-4">Press ESC key or click on ✕ button to close</p>
			</div>
		</dialog>
		<dialog id="create_project_modal" class="modal">
			<div class="modal-box">
				<form method="dialog">
					<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
						✕
					</button>
				</form>
				<h3 class="text-lg font-bold">Create Project Modal</h3>
				<p class="py-4">Press ESC key or click on ✕ button to close</p>
			</div>
		</dialog>
	</aside>
</template>
