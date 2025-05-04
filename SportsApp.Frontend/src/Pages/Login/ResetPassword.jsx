import { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import api from '../../services/api';

function ResetPassword() {
  const [params] = useSearchParams();
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    setEmail(params.get('email') || '');
    setToken(params.get('token') || '');
  }, [params]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setMessage('');

    if (newPassword !== confirm) {
      setError('Passwords do not match');
      return;
    }

    try {
      await api.post('/Auth/reset-password', {
        email,
        token,
        newPassword
      });

      setMessage('Password updated! You can now log in.');
    } catch (err) {
      setError('Failed to reset password.');
    }
  };

  return (
    <div style={{ maxWidth: '400px', margin: 'auto', padding: '1rem' }}>
      <h2>Reset Password</h2>
      <form onSubmit={handleSubmit}>
        <input type="hidden" value={email} />
        <input type="hidden" value={token} />

        <div style={{ marginBottom: '1rem' }}>
          <label>New Password:</label>
          <input
            type="password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            required
            style={{ width: '100%', padding: '0.5rem' }}
          />
        </div>

        <div style={{ marginBottom: '1rem' }}>
          <label>Confirm Password:</label>
          <input
            type="password"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
            required
            style={{ width: '100%', padding: '0.5rem' }}
          />
        </div>

        {message && <p style={{ color: 'green' }}>{message}</p>}
        {error && <p style={{ color: 'red' }}>{error}</p>}

        <button type="submit" style={{ padding: '0.5rem 1rem' }}>Reset Password</button>
      </form>
    </div>
  );
}

export default ResetPassword;
// Compare this snippet from SportsApp.Frontend/src/Pages/Login/Login.jsx:
// import { useState } from 'react';