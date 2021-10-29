<template>
  <div>
    <TheHeader />
    <div class="main-content" style="display: flex; height: 925px">
      <TheNavBar @changeWidthContent="changeWidthContent" page="AdminPage" :a="a"/>
      <router-view/>
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
      a : [1,1,1,1,1],
      contentwidth: "calc(100% - 212px)",
    };
  },
  methods: {
    changeWidthContent(e) {
      if (e) {
        this.contentwidth = "calc(100% - 52px)";
      } else this.contentwidth = "calc(100% - 212px)";
    },
  },
};
</script>

<style>
</style>