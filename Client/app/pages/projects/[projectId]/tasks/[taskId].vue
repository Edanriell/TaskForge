<script lang="ts" setup>
	const route = useRoute();
</script>

<template>
	<section class="px-6 py-12">
		<h1>Task Page</h1>
		<h2>Task page {{ route.params.taskId }}</h2>
		<h3>For project {{ route.params.projectId }}</h3>
	</section>
</template>
