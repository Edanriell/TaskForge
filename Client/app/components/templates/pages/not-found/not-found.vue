<script lang="ts" setup>
	const handleGoHome = () => {
		navigateTo("/");
	};

	const handleGoBack = () => {
		const router = useRouter();
		router.back();
	};
</script>

<template>
	<div class="flex min-h-screen items-center justify-center bg-base-200">
		<div class="text-center">
			<!-- 404 number with icon overlay -->
			<div class="relative mb-10">
				<h1
					class="text-[10rem] font-black leading-none tracking-tighter text-primary opacity-10 select-none"
				>
					404
				</h1>
				<div class="absolute inset-0 flex items-center justify-center">
					<Icon
						name="heroicons:face-frown"
						class="h-20 w-20 animate-bounce text-primary"
					/>
				</div>
			</div>

			<!-- Content -->
			<h2 class="mb-4 text-3xl font-bold">Oops! Page not found</h2>
			<p class="text-base-content/70 mb-10 text-lg">
				The page you're looking for doesn't exist or has been moved.
			</p>

			<!-- Action buttons -->
			<div class="flex justify-center gap-4">
				<button class="btn btn-primary btn-lg gap-2" @click="handleGoHome">
					<Icon name="heroicons:home" class="h-5 w-5" />
					Go Home
				</button>
				<button class="btn btn-outline btn-lg gap-2" @click="handleGoBack">
					<Icon name="heroicons:arrow-left" class="h-5 w-5" />
					Go Back
				</button>
			</div>
		</div>
	</div>
</template>
