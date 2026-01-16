<script setup lang="ts">
	interface Props {
		id: string;
		title: string;
	}

	const { id, title } = defineProps<Props>();
</script>

<template>
	<dialog :id="id" class="modal">
		<div class="modal-box">
			<form method="dialog">
				<button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">✕</button>
			</form>
			<h3 class="text-lg font-bold mb-4">{{ title }}</h3>
			<slot />
		</div>
	</dialog>
</template>
