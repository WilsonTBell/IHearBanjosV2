import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import { Spinner } from 'reactstrap';
import Header from "./components/Header";
import ApplicationViews from "./components/ApplicationViews";
import { onLoginStatusChange } from "./modules/authManager";
import { getCurrentBanjoistByFirebaseId } from './modules/banjoistManager';
import "firebase/auth";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(null);
  const [isAdmin, setIsAdmin] = useState();

  useEffect(() => {
    onLoginStatusChange(setIsLoggedIn)
  }, []);

  useEffect(() => {
    getCurrentBanjoistByFirebaseId()?.then((banjoist) => {
      if (banjoist.userType.name === "Admin") {
        setIsAdmin(true);
      } else {
        setIsAdmin(false);
      }
    });
  }, [isLoggedIn])

  if (isLoggedIn === null) {
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <Router>
      <Header isLoggedIn={isLoggedIn} isAdmin={isAdmin} />
      <ApplicationViews isLoggedIn={isLoggedIn} isAdmin={isAdmin} />
    </Router>
  );
}

export default App;

