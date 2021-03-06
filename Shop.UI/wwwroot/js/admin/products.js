﻿var app = new Vue({
    el: '#app',
    data: {
       
            editing: false,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product description",
                value: 1.99
            },
            loading: false,
            objectIndex: 0,
            products: []
        
    },
    mounted() {
        //se executa doar o data
        this.getProducts();
    },
    methods: {

        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    // this.products = res.data;
                    this.productModel = {
                        id: res.data.id,
                        name: res.data.name,
                        description: res.data.description,
                        value: res.data.value,
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateProduct() {
            this.loading = true;
            axios.put('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.productModel.id = 0;
            this.editing = true;
        },
        editProduct(product, index) {
            this.objectIndex = index;
            this.getProduct(product.id);
            this.editing = true;
        },
        cancel() {
            this.editing = false;
        },

    },
    computed: {

    }
});