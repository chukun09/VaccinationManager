<template>
  <div class="baseinput">
    <p class="titleinput">
      {{ titleinput }}<span v-show="required">(<span id="star">*</span>)</span>
    </p>
    <input
      :type="typeofinput"
      :placeholder="placeholder"
      v-model.trim="v$.inputVal.$model"
      :class="{
        'is-invalid': v$.inputVal.$error && required,
        'is-valid': !v$.inputVal.$invalid,
      }"
      :disabled="validated == 1"
    />
    <span v-if="!v$.inputVal.required && v$.inputVal.$dirty && required"></span>
  </div>
</template>
<script>
import useVuelidate from "@vuelidate/core";
import { required } from "@vuelidate/validators";
export default {
  setup() {
    return { v$: useVuelidate() };
  },
  name: "BaseInput",
  props: {
    validated:{
      required: false,
    },
    formatInput: {
      type: Function,
    },
    required: {
      default: false
    },
    titleinput: {
      type: String,
      required: true,
    },
    placeholder: {
      type: String,
    },
    typeofinput: {
      type: String,
    },
    modelValue: {
      default: "",
    },
  },
  validations() {
    return {
      inputVal: {
        required,
      },
    };
  },
  computed: {
    inputVal: {
      get() {
        return this.modelValue;
      },
      set(val) {
        this.$emit("update:modelValue", val);
      },
    },
  },
};
</script>
<style scoped>
input {
  width: 300px;
  margin-right: 20px;
  height: 40px;
  padding-left: 16px;
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  color: black;
  font-family: "regular";
  font-size: 13px;
  padding-bottom: 4px;
}
input:focus {
  outline: none;
}
.titleinput {
  text-align: left;
  margin: 0px;
  display: flex;
  margin-top: 10px;
}
.is-valid {
  border: 1px solid #019160 !important;
}
.is-invalid {
  border: 1px solid #ff0000 !important;
}
</style>