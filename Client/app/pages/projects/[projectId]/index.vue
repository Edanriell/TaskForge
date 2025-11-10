<script lang="ts" setup>
	const route = useRoute();
</script>

<template>
	<section class="px-6 py-12">
		<h1>Project Page</h1>
		<h2>Project page {{ route.params.projectId }}</h2>
	</section>
</template>
