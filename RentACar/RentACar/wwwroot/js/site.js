﻿html {
    font - size: 14px;
}

@media(min - width: 768px) {
    html {
        font - size: 16px;
    }
}

html {
    position: relative;
    min - height: 100 %;
}

body {
    margin - bottom: 60px;
}

@charset "utf-8";

.carousel - inner {
    width: auto;
    height: 500px;
    max - height: 500px!important;
}

.carousel - content {
    color: black;
    display: flex;
    text - align: center;
}

/* Absolute Center Spinner */
.loading {
    position: fixed;
    display: none;
    z - index: 1031;
    height: 2em;
    width: 2em;
    overflow: show;
    margin: auto;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
}


    /* Transparent Overlay */
    .loading: before {
    content: '';
    display: block;
    position: fixed;
    top: 0;
    left: 0;
    width: 100 %;
    height: 100 %;
    background - color: rgba(0, 0, 0, 0.3);
}

    /* :not(:required) hides these rules from IE9 and below */
    .loading: not(: required) {
    font: 0 / 0 a;
    color: transparent;
    text - shadow: none;
    background - color: transparent;
    border: 0;
}

        .loading: not(: required): after {
    content: '';
    display: block;
    font - size: 10px;
    width: 1em;
    height: 1em;
    margin - top: -0.5em;
    -webkit - animation: spinner 1500ms infinite linear;
    -moz - animation: spinner 1500ms infinite linear;
    -ms - animation: spinner 1500ms infinite linear;
    -o - animation: spinner 1500ms infinite linear;
    animation: spinner 1500ms infinite linear;
    border - radius: 0.5em;
    -webkit - box - shadow: rgba(0, 0, 0, 0.75) 1.5em 0 0 0, rgba(0, 0, 0, 0.75) 1.1em 1.1em 0 0, rgba(0, 0, 0, 0.75) 0 1.5em 0 0, rgba(0, 0, 0, 0.75) - 1.1em 1.1em 0 0, rgba(0, 0, 0, 0.5) - 1.5em 0 0 0, rgba(0, 0, 0, 0.5) - 1.1em - 1.1em 0 0, rgba(0, 0, 0, 0.75) 0 - 1.5em 0 0, rgba(0, 0, 0, 0.75) 1.1em - 1.1em 0 0;
    box - shadow: rgba(0, 0, 0, 0.75) 1.5em 0 0 0, rgba(0, 0, 0, 0.75) 1.1em 1.1em 0 0, rgba(0, 0, 0, 0.75) 0 1.5em 0 0, rgba(0, 0, 0, 0.75) - 1.1em 1.1em 0 0, rgba(0, 0, 0, 0.75) - 1.5em 0 0 0, rgba(0, 0, 0, 0.75) - 1.1em - 1.1em 0 0, rgba(0, 0, 0, 0.75) 0 - 1.5em 0 0, rgba(0, 0, 0, 0.75) 1.1em - 1.1em 0 0;
}

/* Animation */
@-webkit - keyframes spinner {
    0 % {
        - webkit - transform: rotate(0deg);
    -moz - transform: rotate(0deg);
    -ms - transform: rotate(0deg);
    -o - transform: rotate(0deg);
    transform: rotate(0deg);
}

100 % {
        - webkit - transform: rotate(360deg);
-moz - transform: rotate(360deg);
-ms - transform: rotate(360deg);
-o - transform: rotate(360deg);
transform: rotate(360deg);
    }
}

@-moz - keyframes spinner {
    0 % {
        - webkit - transform: rotate(0deg);
    -moz - transform: rotate(0deg);
    -ms - transform: rotate(0deg);
    -o - transform: rotate(0deg);
    transform: rotate(0deg);
}

100 % {
        - webkit - transform: rotate(360deg);
-moz - transform: rotate(360deg);
-ms - transform: rotate(360deg);
-o - transform: rotate(360deg);
transform: rotate(360deg);
    }
}

@-o - keyframes spinner {
    0 % {
        - webkit - transform: rotate(0deg);
    -moz - transform: rotate(0deg);
    -ms - transform: rotate(0deg);
    -o - transform: rotate(0deg);
    transform: rotate(0deg);
}

100 % {
        - webkit - transform: rotate(360deg);
-moz - transform: rotate(360deg);
-ms - transform: rotate(360deg);
-o - transform: rotate(360deg);
transform: rotate(360deg);
    }
}

@keyframes spinner {
    0 % {
        - webkit - transform: rotate(0deg);
    -moz - transform: rotate(0deg);
    -ms - transform: rotate(0deg);
    -o - transform: rotate(0deg);
    transform: rotate(0deg);
}

100 % {
        - webkit - transform: rotate(360deg);
-moz - transform: rotate(360deg);
-ms - transform: rotate(360deg);
-o - transform: rotate(360deg);
transform: rotate(360deg);
    }
}

.field - validation - error {
    color: #f00;
}

.field - validation - valid {
    display: none;
}

.input - validation - error {
    border: 1px solid #f00;
    background - color: #fee;
}

.validation - summary - errors {
    font - weight: bold;
    color: #f00;
}

.validation - summary - valid {
    display: none;
}

.text - danger span:: before {
    content: "";
    display: block;
    background: url('/images/error.png');
    width: 20px;
    height: 20px;
    float: left;
    margin: 0 6px 0 0;
}
