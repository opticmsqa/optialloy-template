// example of editor that is not implemented in dojo, but using importing modules mechanism to get code
export default function customEditor(editorContainer, initialValue, onEditorValueChange, widgetSettings, readOnly) {
    return {
        render: function () {
            console.log("rendering custom widget");

            const input = document.createElement("input");
            input.type = "text";
            if (readOnly) {
                input.disabled = true;
            }
            input.value = initialValue || "";
            input.onchange = (event) => onEditorValueChange(event.target.value);
            editorContainer.appendChild(input);
            this._input = input;
        },
        updateValue: function (value) {
            console.log("updating custom widget value");
            this._input.value = value;
        },
        destroy: function () {
            console.log("removing custom widget");
        },
    };
}
