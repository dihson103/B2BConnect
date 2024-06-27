import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
export default function LoginForm() {
  return (
    <div className='flex min-h-screen items-center justify-center'>
      <Card className='w-full max-w-sm'>
        <CardHeader>
          <CardTitle className='text-2xl'>Đăng nhập</CardTitle>
        </CardHeader>
        <CardContent>
          <div className='grid gap-4'>
            <div className='grid gap-2'>
              <Label htmlFor='email'>Email</Label>
              <Input id='email' type='email' placeholder='m@example.com' required />
            </div>
            <div className='grid gap-2'>
              <div className='flex items-center'>
                <Label htmlFor='password'>Mật khẩu</Label>
              </div>
              <Input id='password' type='password' required />
            </div>
            <Button type='submit' className='w-full'>
              Đăng nhập
            </Button>
          </div>
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
