

export const getAllTypes = () => {
    return fetch('/api/type')
        .then((res) => res.json())
};