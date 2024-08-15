import React, { useState } from 'react';

function Navigation(props) {
    const [color, setColor] = useState("red");

    return
    <h1>
        Ima { props.name }
    </h1>
}

export default Navigation;