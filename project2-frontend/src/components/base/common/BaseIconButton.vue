<template>
  <div class="icon-button">
    <button id="buttonadd" type="button" @click="action">
    <img :src="require(`@/assets/${icon}`)" id="imgadd"/>{{ title }}
    </button>
  </div>
</template>
<style scoped>
</style>
<script>
export default {
  name: "IconButton",
  props: {
    icon: {required: true },
    title: { type: String, required: true },
    action: {
      type: Function,
    },
  },
  data() {
    return {};
  },
};
</script>