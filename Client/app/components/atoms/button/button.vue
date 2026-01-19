<script lang="ts" setup>
	type ButtonColors = "black" | "blue" | "neutral";

	interface Props {
		color: ButtonColors;
		classes?: string;
		outline?: boolean;
	}

	const { color, classes = "", outline = false } = defineProps<Props>();

	const emit = defineEmits<{
		click: [];
	}>();
</script>

<template>
	<button
		v-if="color === 'black'"
		:class="['btn btn-neutral', { 'btn-outline': outline }, classes]"
		@click="emit('click')"
	>
		<slot />
	</button>
	<button
		v-else-if="color === 'blue'"
		:class="['btn btn-primary', { 'btn-outline': outline }, classes]"
		@click="emit('click')"
	>
		<slot />
	</button>
	<button
		v-else-if="color === 'neutral'"
		:class="['btn', { 'btn-outline': outline }, classes]"
		@click="emit('click')"
	>
		<slot />
	</button>
</template>
