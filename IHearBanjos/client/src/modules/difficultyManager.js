
export const getAllDifficulties = () => {
    return fetch('/api/difficulty')
        .then((res) => res.json())
};