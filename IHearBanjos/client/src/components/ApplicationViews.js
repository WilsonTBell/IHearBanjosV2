import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import { Banjoists } from "./Banjoists";
import { AllTabs } from "./AllTabs";
import { MyTabs } from "./MyTabs";
import { FavoriteTabs } from "./FavoriteTabs";
import { TabDetail } from "./TabDetail";
import { TabForm } from "./TabForm";
import { TabEdit } from "./TabEdit";

export default function ApplicationViews({ isLoggedIn, isAdmin }) {
    return (
        <main>
            <Routes>
                <Route path="/">
                    <Route
                        index
                        element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
                    />
                    <Route path="login" element={<Login />} />
                    <Route path="register" element={<Register />} />
                    <Route
                        path="profiles"
                        element={!isLoggedIn ? <Navigate to="/login" /> :
                            isAdmin ? <Banjoists /> : <h1>Access Denied!</h1>}
                    />
                    <Route path="tab/add" element={isLoggedIn ? <TabForm /> : <Navigate to="/login" />} />
                    <Route path="tab/:tabId" element={isLoggedIn ? <TabDetail /> : <Navigate to="/login" />} />
                    <Route path="tab/:tabId" element={isLoggedIn ? <TabDetail /> : <Navigate to="/login" />} />
                    <Route path="tab/edit/:tabId" element={isLoggedIn ? <TabEdit /> : <Navigate to="/login" />} />
                    <Route path="tab" element={isLoggedIn ? <AllTabs /> : <Navigate to="/login" />} />
                    <Route path="mytabs" element={isLoggedIn ? <MyTabs /> : <Navigate to="/login" />} />
                    <Route path="favorites" element={isLoggedIn ? <FavoriteTabs /> : <Navigate to="/login" />} />
                    <Route path="*" element={<p>Whoops, nothing here...</p>} />
                </Route>
            </Routes>
        </main>
    );
};