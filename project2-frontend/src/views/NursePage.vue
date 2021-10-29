<template>
  <div>
    <TheHeader />
    <div class="main-content" style="display: flex; height: 925px">
      <TheNavBar page="NursePage" :a="a" />
      <router-view />
    </div>
  </div>
</template>
<script>
import TheHeader from "./../components/layout/common/TheHeader.vue";
import TheNavBar from "./../components/layout/common/TheNavBar.vue";
export default {
  components: {
    TheHeader,
    TheNavBar,
  },
  data() {
    return {
      a: [0, 1, 1, 0, 1],
    };
  },
};
</script>

<style>

</style>