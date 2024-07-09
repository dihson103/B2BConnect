'use client'

import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { loginAction } from '@/actions/auth.actions'
import { useToast } from '@/components/ui/use-toast'
import { useState } from 'react'
import { apiErrorHandler } from '@/lib/utils'
import { LoginType } from '@/types/auth.types'

export default function LoginForm() {
  const { toast } = useToast()
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const handleLogin = () => {
    const data = {
      email, 
      password
    }

    loginAction(data)
      .then((data) => {
        toast({
          title: 'Login success',
          description: 'Welcome back',
          duration: 5000
        })
      })
      .catch((error: Error) => {
        apiErrorHandler(error.message)
      })  
    }

  return (
    <div className='flex min-h-screen items-center justify-center'>
      <Card className='w-full max-w-sm'>
        <CardHeader>
          <CardTitle className='text-2xl'>Đăng nhập</CardTitle>
        </CardHeader>
        <CardContent>
          <form>
            <div className='grid gap-4'>
              <div className='grid gap-2'>
                <Label htmlFor='email'>Email</Label>
                <Input
                  id='email'
                  type='text'
                  placeholder='m@example.com'
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
              <div className='grid gap-2'>
                <div className='flex items-center'>
                  <Label htmlFor='password'>Mật khẩu</Label>
                </div>
                <Input
                  id='password'
                  type='password'
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
              </div>
              <Button type='button' className='w-full' onClick={handleLogin}>
                Đăng nhập
              </Button>
            </div>
          </form>
          <div className='mt-4 text-center text-sm'>
            Chưa có tài khoản?{' '}
            <Link href='/register' className='underline'>
              Đăng ký
            </Link>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
