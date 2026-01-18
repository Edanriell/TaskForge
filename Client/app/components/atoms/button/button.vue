<script lang="ts" setup>
	type ButtonColors = "black" | "blue";

	interface Props {
		color: ButtonColors;
	}

	const { color } = defineProps<Props>();

	const emit = defineEmits<{
		click: [];
	}>();
</script>

<template>
	<button v-if="color === 'black'" class="btn btn-sm btn-neutral w-full" @click="emit('click')">
		<slot />
	</button>
	<button v-else-if="color === 'blue'" class="btn btn-primary w-full"><slot /></button>
</template>
