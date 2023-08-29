<template>
  <div class="text-center">
    <div class="text-h2 text-dark q-mt-md">Index</div>
  </div>


</template>

<script>
  import { useRouter } from "vue-router";
  import { onMounted } from "vue"

  export default {
    setup() {
      const router = useRouter();

      onMounted(() => {
        router.push("Scores");
      });
    },
  }
</script>
