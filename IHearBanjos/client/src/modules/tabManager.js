import { getToken } from "./authManager";

const baseUrl = '/api/tab';

export const getAllTabs = () => {
    return fetch('/api/tab')
        .then((res) => res.json())
};

export const getTabById = (tabId) => {
    return fetch(`/api/tab/${tabId}`)
        .then((res) => res.json())
};

export const getMyTabs = () => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/MyTabs`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                return Error("An unknown error occured");
            }
        })
    })
};

export const getFavoriteTabs = () => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/FavoriteTabs`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                return Error("An unknown error occured");
            }
        })
    })
};

export const addTab = (tab) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(tab),
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else if (resp.status === 401) {
                throw new Error("Unauthorized");
            } else {
                throw new Error(
                    "An unknown error occurred while trying to save a new tab.",
                );
            }
        });
    });
};

export const editTab = (tab) => {

    return fetch(baseUrl + `/${tab.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(tab),
    });
};

export const deleteTab = (tabId) => {
    return fetch(baseUrl + `/${tabId}`, {
        method: "DELETE"
    });
};

export const deleteFavorite = (tabId) => {
    return getToken().then((token) => {
        return fetch(baseUrl + `/favorite/${tabId}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    })
};

export const addFavorite = (banjoistFavorite) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/favorite`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(banjoistFavorite),
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else if (resp.status === 401) {
                throw new Error("Unauthorized");
            } else {
                throw new Error(
                    "An unknown error occurred while trying to save a new tab.",
                );
            }
        });
    });
};